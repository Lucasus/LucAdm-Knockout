namespace KnockAdm
{
    public static class OperationResponseExtensions
    {
        /// <summary>
        /// Creates generic operation response with target object as generic operation result
        /// </summary>
        public static OperationResponse<T> AsResponse<T>(this T result)
        {
            return new OperationResponse<T>(result);
        }
    }
}