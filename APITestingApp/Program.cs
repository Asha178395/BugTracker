using System;
using System.IO;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;

public class API
{
    public string endPoint { get; set; }
    public object input { get; set; }
    public object output { get; set; }

    public string method { get; set; }
}

public class ListOfAPI
{
    public List<API> APIs { get; set;}
}

public class Program
{
    
    public static void Main()
    {
        // Remove local json file. Attach to solution. Find the root folder and get the file.
        string fileName = "C:\\Users\\SYR00415\\source\\repos\\BugTracker\\APITestingApp\\APIs.json";

        // Read the entire JSON file as a string
        string jsonString = File.ReadAllText(fileName);
        var endPoints = JsonConvert.DeserializeObject<ListOfAPI>(jsonString);
        foreach (var point in endPoints.APIs)
        {
            // Call the API and compare the response with the expected output
            var response = "1";
            if (point.method == "POST")
            {
                response = CallPostApi(point.endPoint, point.input);
            }else if ( point.method == "GET")
            {
                response = CallGetApi(point.endPoint, point.input);
            }
            else
            {
                response = CallPutApi(point.endPoint, point.input);
            }
            var expectedOutput = point.output;

            // Deserialize the API response and compare it with the expected output
            var apiResponse = JsonConvert.DeserializeObject(response);
            //var expectedResponse = JsonConvert.DeserializeObject(expectedOutput);

            bool isOutputMatching = AreObjectsEqual(apiResponse, expectedOutput);

            Console.WriteLine($"API Endpoint: {point.endPoint}");
            Console.WriteLine($"Output Matching: {isOutputMatching}");
            Console.WriteLine();
        }

      

        //var baseURL = "https://bugtrackerwebapp123.azurewebsites.net/"; // Move this to config file [app.json file]

        //var api = endPoints.APIs; // Get this collection in List<>

        //var client = new RestClient(baseURL + api[1].endPoint.Substring(0, api[1].endPoint.LastIndexOf("/"))); // Avoid sub_strings
        //var request = new RestRequest(api[1].endPoint.Substring(api[1].endPoint.LastIndexOf("/")), Method.Put);
        //request.AddJsonBody(api[1].input);
        //var response = client.Execute(request);
        //string outoput = (response.Content).ToString();
        //Root2 commentsData = JsonConvert.DeserializeObject<Root2>(response.Content) ;
        //Assert.AreEqual( api[1].output.commentId, commentsData.commentId);

        //Assert.IsNotNull(commentsData);

        /*if(api[1].output.commentId== commentsData.commentId)
         {
             Console.WriteLine("True");
         }
        else
         {
             Console.WriteLine("False");
         }*/

    }

    static string CallPostApi(string endpoint, object input)
    {
        // Serialize the input object to JSON
        string jsonInput = JsonConvert.SerializeObject(input);

        // Make the HTTP request to the API endpoint
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.PostAsync(endpoint, new StringContent(jsonInput)).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return responseContent;
        }
    }
    static string CallPutApi(string endpoint, object input)
    {
        string jsonInput = JsonConvert.SerializeObject(input);

        // Set the content type and create the StringContent object with the JSON string
        var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");

        // Send the PUT request with the content
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.PutAsync(endpoint,content).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return responseContent;
        }
    }
    static string CallGetApi(string endpoint, object input)
    {
        int count = 0;
        Type inputType = typeof(input);
        foreach (var property in inputType.GetProperties())
        {
            if (count == 0)
            {
                endpoint += ("?" + property.Name + "=" + property.GetValue(input));
                Console.WriteLine(("?" + property.Name+ "=" + property.GetValue(input)));
            }
            else
            {

                //object propertyValue = property.GetValue(input);
                endpoint += ("&" + property.Name + "=" + property.GetValue(input,null));
            }
            count++;

        }
        // Make the HTTP request to the API endpoint
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.GetAsync(endpoint).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return responseContent;
        }
    }

    static bool AreObjectsEqual(object obj1, object obj2) //Works for Get and Put methods if objects doesn't have dynamic properties
    {
        string serializedObj1 = JsonConvert.SerializeObject(obj1);
        string serializedObj2 = JsonConvert.SerializeObject(obj2);
        return serializedObj1 == serializedObj2;
    }

    static bool AreCommentObjectsEqual(object obj1, object obj2) //Works for Get and Put methods if objects doesn't have dynamic properties
    {
        string serializedObj1 = JsonConvert.SerializeObject(obj1);
        string serializedObj2 = JsonConvert.SerializeObject(obj2);
        return serializedObj1 == serializedObj2;
    }

   



}
