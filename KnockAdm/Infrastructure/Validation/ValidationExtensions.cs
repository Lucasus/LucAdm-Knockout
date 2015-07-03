using System;
using FluentValidation;
using System.Threading.Tasks;

namespace KnockAdm
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Validates command with validator and creates validation result with validation errors
        /// </summary>
        public static Task<ValidationResult> Validate<TCommand>(this TCommand command, AbstractValidator<TCommand> validator)
        {
            var validationResult = new ValidationResult();
            foreach (var error in validator.Validate(command).Errors)
            {
                validationResult.AddError(error.PropertyName, error.ErrorMessage);
            }
            return Task.FromResult(validationResult);
        }
       
        /// <summary>
        /// Checks rule but only if validation result is still valid, and adds rule validation errors to it
        /// </summary>
        public static async Task<ValidationResult> CheckAsync(this Task<ValidationResult> validationTask, params IRule[] rules)
        {
            var validationResult = await validationTask.ConfigureAwait(false);
            if (validationResult.IsValid)
            {
                foreach (var rule in rules)
                {
                    if (!(await rule.CheckAsync().ConfigureAwait(false)))
                    {
                        validationResult.AddError(rule.Name, rule.ErrorMessage);
                    }
                }
            }
            return validationResult;
        }

        /// <summary>
        /// Executes operation if a validation result contains no errors and returns response with this validation result
        /// </summary>
        public static Task<OperationResponse> IfValidAsync(this Task<ValidationResult> validationTask, Func<Task> operation)
        {
            return IfValidAsync(validationTask, result => operation());
        }

        /// <summary>
        /// Executes operation if a validation result contains no errors and returns response with this validation result
        /// </summary>
        public async static Task<OperationResponse> IfValidAsync(this Task<ValidationResult> validationTask, Func<ValidationResult, Task> operation)
        {
            var validationResult = await validationTask.ConfigureAwait(false);
            if (validationResult.IsValid)
            {
                await operation(validationResult).ConfigureAwait(false);
            }
            return new OperationResponse(validationResult);
        }

        /// <summary>
        /// Executes operation if a validation result contains no errors and returns response with this validation result
        /// </summary>
        public static Task<OperationResponse> IfValidAsync(this Task<ValidationResult> validationTask, Func<Task<OperationResponse>> function)
        {
            return IfValidAsync(validationTask, result => function());
        }

        /// <summary>
        /// Executes operation if a validation result contains no errors and returns response with this validation result
        /// </summary>
        public async static Task<OperationResponse> IfValidAsync(this Task<ValidationResult> validationTask, Func<ValidationResult, Task<OperationResponse>> function)
        {
            var validationResult = await validationTask.ConfigureAwait(false);
            if (validationResult.IsValid)
            {
                return await function(validationResult).ConfigureAwait(false);
            }
            return new OperationResponse(validationResult);
        }

        /// <summary>
        /// Executes operation if a validation result contains no errors and returns response with this validation result
        /// </summary>
        public static Task<OperationResponse<TResult>> IfValidAsync<TResult>(this Task<ValidationResult> validationTask, 
            Func<Task<OperationResponse<TResult>>> function)
        {
            return IfValidAsync(validationTask, result => function());
        }

        /// <summary>
        /// Executes operation if a validation result contains no errors and returns response with this validation result
        /// </summary>
        public static async Task<OperationResponse<TResult>> IfValidAsync<TResult>(this Task<ValidationResult> validationTask, Func<ValidationResult, 
            Task<OperationResponse<TResult>>> operation)
        {
            var validationResult = await validationTask.ConfigureAwait(false);
            if (validationResult.IsValid)
            {
                return await operation(validationResult).ConfigureAwait(false);
            }
            return new OperationResponse<TResult>(default(TResult), validationResult);
        }

    }
}