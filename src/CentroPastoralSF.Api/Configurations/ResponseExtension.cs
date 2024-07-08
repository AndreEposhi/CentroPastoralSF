using CentroPastoralSF.Core.Responses;
using System.Net;

namespace CentroPastoralSF.Api.Configurations
{
    public static class ResponseExtension
    {
        public static IResult ToResult<TResponse>(this Response<TResponse> response, string? url = null) where TResponse : class
        {
            if (response.StatusCode == HttpStatusCode.OK && response.Success)
            {
                return ResponseOk(response);
            }

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return ResponseCreated(url, response);
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return ResponseBadRequest(response);
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return ResponseNoContent();
            }

            //Todo: Manter e .Net 9 implementar o InternalServerError
            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
            { 
                return ResponseUnprocessableEntity(response);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return ResponseNotFound(response);
            }

            return ResponseOk();
        }

        private static IResult ResponseOk()
        {
            return TypedResults.Ok();
        }

        private static IResult ResponseOk<TResponse>(TResponse response)
        {
            return TypedResults.Ok(response);
        }

        private static IResult ResponseCreated()
        {
            return TypedResults.Created();
        }

        private static IResult ResponseCreated<TResponse>(string? uri, TResponse response)
        {
            return TypedResults.Created($"{uri}", response);
        }

        private static IResult ResponseBadRequest<TResponse>(Response<TResponse> response) where TResponse : class
        {
            return TypedResults.BadRequest(response);
        }

        private static IResult ResponseNoContent()
        {
            return TypedResults.NoContent();
        }

        private static IResult ResponseUnprocessableEntity<TResponse>(Response<TResponse> response) where TResponse : class
        {
            return TypedResults.UnprocessableEntity(response);
        }

        private static IResult ResponseNotFound<TResponse>(Response<TResponse> response) where TResponse : class

        {
            return TypedResults.NotFound(response); 
        }
    }
}