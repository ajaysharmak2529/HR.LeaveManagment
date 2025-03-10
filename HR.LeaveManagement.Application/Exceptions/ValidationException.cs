﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();
        public ValidationException(ValidationResult validationResult)
        {
            Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
