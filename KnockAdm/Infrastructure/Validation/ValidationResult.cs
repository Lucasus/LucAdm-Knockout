using System.Collections.Generic;

namespace KnockAdm
{
    /// <summary>
    /// Contains collection of validation/business rules errors
    /// </summary>
    public class ValidationResult
    {
        public ValidationResult()
        {
            Errors = new Dictionary<string, IList<string>>();
            GlobalErrors = new List<string>();
        }

        public IDictionary<string, IList<string>> Errors { get; private set; }
        public IList<string> GlobalErrors { get; protected set; }

        public bool IsValid
        {
            get { return Errors.Count == 0 && GlobalErrors.Count == 0; }
        }

        public void AddError(string key, string message)
        {
            if (key.Equals(string.Empty))
            {
                if (!GlobalErrors.Contains(message))
                {
                    GlobalErrors.Add(message);
                }
            }
            else
            {
                if (!Errors.ContainsKey(key))
                {
                    Errors.Add(key, new List<string>());
                }
                if (!Errors[key].Contains(message))
                {
                    Errors[key].Add(message);
                }
            }
        }

        public void AddGlobalError(string message)
        {
            AddError(string.Empty, message);
        }
    }
}