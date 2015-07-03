namespace KnockAdm
{
    /// <summary>
    /// Operation response consisting of validation result and custom generic result object
    /// </summary>
    /// <typeparam name="TResult">Custom result</typeparam>
    public class OperationResponse<TResult> : OperationResponse
    {
        public OperationResponse(TResult result, ValidationResult validationResult = null)
            : base(validationResult)
        {
            Result = result;
        }

        public TResult Result { get; private set; }
    }

}