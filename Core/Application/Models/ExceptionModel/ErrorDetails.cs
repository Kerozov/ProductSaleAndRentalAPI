﻿namespace Application.Models.ExceptionModel;

public class ErrorDetails :Exception
{
    public int StatusCode  { get; set; }

    public string Message { get; set; }
    
    
}