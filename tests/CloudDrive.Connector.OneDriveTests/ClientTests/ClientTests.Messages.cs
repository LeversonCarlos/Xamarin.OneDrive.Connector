using System;
using System.Net.Http;
using Xunit;

namespace Xamarin.CloudDrive.Connector.OneDriveTests
{
   partial class Client
   {

      [Fact]
      public async void GetAsync_WithException_MustThrowException()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var exception = new Exception("Some dummy exception");
         var httpClient = HandlerBuilder.Create().WithException(exception).Build();
         var client = new OneDriveClient(token, httpClient);

         var expected = exception;
         var value = await Assert.ThrowsAsync<Exception>(async () => await client.GetAsync(requestUri));

         Assert.NotNull(value);
         Assert.Equal(expected.Message, value.Message);
      }

      [Fact]
      public async void GetAsync_WithSuccess_MustResultSpectedValue()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var expected = new ProfileVM { ID = "Some Test ID" };
         var httpClient = HandlerBuilder.Create().WithResult(expected).Build();
         var client = new OneDriveClient(token, httpClient);

         var value = await client.GetAsync<ProfileVM>(requestUri);

         Assert.Equal(expected.ID, value.ID);
      }

      [Fact]
      public async void PutAsync_WithException_MustThrowException()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var exception = new Exception("Some dummy exception");
         var httpClient = HandlerBuilder.Create().WithException(exception).Build();
         var client = new OneDriveClient(token, httpClient);

         var expected = exception;
         var value = await Assert.ThrowsAsync<Exception>(async () => await client.PutAsync(requestUri, new StringContent("")));

         Assert.NotNull(value);
         Assert.Equal(expected.Message, value.Message);
      }

      [Fact]
      public async void PostAsync_WithException_MustThrowException()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var exception = new Exception("Some dummy exception");
         var httpClient = HandlerBuilder.Create().WithException(exception).Build();
         var client = new OneDriveClient(token, httpClient);

         var expected = exception;
         var value = await Assert.ThrowsAsync<Exception>(async () => await client.PostAsync(requestUri, new StringContent("")));

         Assert.NotNull(value);
         Assert.Equal(expected.Message, value.Message);
      }

      [Fact]
      public async void PostAsync_WithSuccess_MustResultSpectedValue()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var expected = new ProfileVM { ID = "Some Test ID" };
         var httpClient = HandlerBuilder.Create().WithResult(expected).Build();
         var client = new OneDriveClient(token, httpClient);

         var value = await client.PostAsync<ProfileVM>(requestUri, new StringContent(""));

         Assert.Equal(expected.ID, value.ID);
      }

      [Fact]
      public async void PostAsync_WithSuccessOnSecondOverload_MustResultSpectedValue()
      {
         var token = TokenBuilder.Create().Build();
         var requestUri = "someDummyUrl";
         var expected = new ProfileVM { ID = "Some Test ID" };
         var httpClient = HandlerBuilder.Create().WithResult(expected).Build();
         var client = new OneDriveClient(token, httpClient);

         var value = await client.PostAsync<ProfileVM, ProfileVM>(requestUri, expected);

         Assert.Equal(expected.ID, value.ID);
      }

      [Fact]
      public async void GetValueAsync_WithException_MustThrowException()
      {
         var token = TokenBuilder.Create().Build();
         var exception = new Exception("Some dummy exception");
         var httpClient = HandlerBuilder.Create().WithException(exception).Build();
         var client = new OneDriveClient(token, httpClient);

         var messageResponse = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
         messageResponse.Content = new StringContent(exception.Message);
         var value = await Assert.ThrowsAsync<Exception>(async () => await client.GetValueAsync<ProfileVM>(messageResponse));

         Assert.NotNull(value);
         Assert.Equal(exception.Message, value.Message);
      }

   }
}
