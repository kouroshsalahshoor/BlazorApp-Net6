﻿namespace BlazorApp.Server.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
