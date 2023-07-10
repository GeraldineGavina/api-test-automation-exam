
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using TestAPI.ProductFolder;
using TestAPI.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace TestAPI
{
    public class Tests
    {
        private RestClient client;
        private string name = "name" + Util.GenerateCurrentDateAndTime();
        private string email = "email" + Util.GenerateCurrentDateAndTime() + "@test.com";

        [OneTimeSetUp]
        public void Setup()
        {
            client = RestClientFactory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client = null;
        }

        [Test, Order(0)]
        public void GetAllProductList()
        {
            //API 1: Get All Products List
            RestRequest request = new RestRequest("/productsList");
            RestResponse response = client.Get(request);
            ProductResponse productResponse = JsonConvert.DeserializeObject<ProductResponse>(response.Content);
            Assert.AreEqual(200, productResponse.responseCode);
            Assert.IsNotNull(productResponse.products);

        }

        [Test, Order(1)]
        public void PostToAllProductList()
        {
            //API 2: POST To All Products List
            RestRequest request = new RestRequest("/productsList");
            RestResponse response = client.Post(request);

            Response errorResponse = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(405, errorResponse.responseCode);
            Assert.AreEqual("This request method is not supported.", errorResponse.message);
            Console.WriteLine(response.Content);

        }

        [Test, Order(2)]
        public void GetAllBrandsList()
        {
            //API 3: Get All Brands List
            RestRequest request = new RestRequest("/brandsList");
            RestResponse response = client.Get(request);
            BrandResponse brandResponse = JsonConvert.DeserializeObject<BrandResponse>(response.Content);
            Assert.AreEqual(200, brandResponse.responseCode);
            Console.WriteLine(JsonConvert.SerializeObject(brandResponse.brands));

        }

        [Test, Order(3)]
        public void PutToAllBrands()
        {
            //API 4: PUT To All Brands List
            RestRequest request = new RestRequest("/brandsList");
            RestResponse response = client.Put(request);
            Response errorResponse = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(405, errorResponse.responseCode);
            Assert.AreEqual("This request method is not supported.", errorResponse.message);
            Console.WriteLine(response.Content);
        }

        [Test, Order(4)]
        public void PostToSearchProduct()
        {
            //API 5: POST To Search Product
            RestRequest request = new RestRequest("/searchProduct");
            request.AddParameter("search_product ", "top", ParameterType.RequestBody);
            RestResponse response = client.Post(request);

            ProductResponse productResponse = JsonConvert.DeserializeObject<ProductResponse>(response.Content);
            Assert.AreEqual(200, productResponse.responseCode);
            Console.WriteLine(JsonConvert.SerializeObject(productResponse.products));

        }
        [Test, Order(5)]
        public void PostSearchProductWithoutParameter()
        {
            //API 6: POST To Search Product without search_product parameter
            RestRequest request = new RestRequest("/searchProduct");
            RestResponse response = client.Post(request);
            Response errorResponse = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(400, errorResponse.responseCode);
            Assert.AreEqual("Bad request, search_product parameter is missing in POST request.", errorResponse.message);
            Console.WriteLine(response.Content);
        }

        [Test, Order(6)]
        public void PostToVerifyLoginWithValidDetails()
        {
            //API 7: POST To Verify Login with valid details
            RestRequest request = new RestRequest("/verifyLogin");
            request.AddParameter("email", "user@exist.com");
            request.AddParameter("password", "1");
            RestResponse response = client.Post(request);
            Response loginResponse = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(200, loginResponse.responseCode);
            Console.WriteLine(response.Content);
        }

        [Test, Order(7)]
        public void PostToVerifyLoginWithoutEmailParameter()
        {
            //API 8: POST To Verify Login without email parameter
            RestRequest request = new RestRequest("/verifyLogin");
            request.AddParameter("password", "1");
            RestResponse response = client.Post(request);
            Response errorLogin = JsonConvert.DeserializeObject<Response>(response.Content);

            Assert.AreEqual(400, errorLogin.responseCode);
            Assert.AreEqual("Bad request, email or password parameter is missing in POST request.", errorLogin.message);
            Console.WriteLine(response.Content);
        }
        [Test, Order(8)]
        public void DeleteToVerifyLogin()
        {
            //API 9: DELETE To Verify Login
            RestRequest request = new RestRequest("/verifyLogin");
            RestResponse response = client.Delete(request); ;
            Response errorDeleteLogin = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(405, errorDeleteLogin.responseCode);
            Assert.AreEqual("This request method is not supported.", errorDeleteLogin.message);
            Console.WriteLine(response.Content);
        }
        [Test, Order(9)]
        public void PostToVerifyLoginWithInvalidDetails()
        {
            // API 10: POST To Verify Login with invalid details
            RestRequest request = new RestRequest("/verifyLogin");
            request.AddParameter("email", "no.existing@user.com");
            request.AddParameter("password", "1");
            RestResponse response = client.Post(request);
            Response loginInvalidResponse = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(404, loginInvalidResponse.responseCode);
            Assert.AreEqual("User not found!", loginInvalidResponse.message);
            Console.WriteLine(response.Content);

        }

        [Test, Order(10)]
        public void PostCreateRegisterUserAccount()
        {
            //API 11: POST To Create/Register User Account
            RestRequest request = new RestRequest("/createAccount");
            request.AddParameter("name", name);
            request.AddParameter("email", email);
            request.AddParameter("password", "1");
            request.AddParameter("title", "Mr");
            request.AddParameter("birth_date", "1");
            request.AddParameter("birth_month", "1");
            request.AddParameter("birth_year", "2001");
            request.AddParameter("firstname", "new");
            request.AddParameter("lastname", "user");
            request.AddParameter("company", "company1");
            request.AddParameter("address1", "address1");
            request.AddParameter("address2", "address2");
            request.AddParameter("country", "Canada");
            request.AddParameter("zipcode", "123");
            request.AddParameter("state", "state 1");
            request.AddParameter("city", "city1");
            request.AddParameter("mobile_number", "12345");

            RestResponse response = client.Post(request);
            Response createUser = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(201, createUser.responseCode);
            Assert.AreEqual("User created!", createUser.message);
            Console.WriteLine(response.Content);
        }

        [Test, Order(11)]

        public void PutMethodToUpdateUserAccount()
        {
            RestRequest request = new RestRequest("/updateAccount");
            request.AddParameter("name", name);
            request.AddParameter("email", email);
            request.AddParameter("password", "1");
            request.AddParameter("title", "Mrs");
            request.AddParameter("birth_date", "2");
            request.AddParameter("birth_month", "2");
            request.AddParameter("birth_year", "2002");
            request.AddParameter("firstname", "update");
            request.AddParameter("lastname", "user");
            request.AddParameter("company", "company update");
            request.AddParameter("address1", "address1 update");
            request.AddParameter("address2", "address2 update");
            request.AddParameter("country", "India");
            request.AddParameter("zipcode", "456");
            request.AddParameter("state", "state123");
            request.AddParameter("city", "city update");
            request.AddParameter("mobile_number", "987654321");

            RestResponse response = client.Put(request);
            Response updateUser = JsonConvert.DeserializeObject<Response>(response.Content);
            Assert.AreEqual(200, updateUser.responseCode);
            Assert.AreEqual("User updated!", updateUser.message);
            Console.WriteLine(response.Content);
        }

        [Test, Order(12)]
        public void DeleteMethodToDEleteUserAccount()
        {
            RestRequest request= new RestRequest("/deleteAccount");
            request.AddParameter("email", "new@user.xyz");
            request.AddParameter("password", "1");
            
            RestResponse response = client.Delete(request);
            Response deleteUser = JsonConvert.DeserializeObject<Response>(response.Content);          

        }


    }
}