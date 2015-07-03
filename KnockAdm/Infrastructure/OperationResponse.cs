namespace KnockAdm
{
    /// <summary>
    /// Operation response with validation result
    /// </summary>
    public class OperationResponse
    {
        public OperationResponse(ValidationResult validationResult = null)
        {
            ValidationResult = validationResult ?? new ValidationResult();
        }

        public ValidationResult ValidationResult { get; protected set; }
    }
}