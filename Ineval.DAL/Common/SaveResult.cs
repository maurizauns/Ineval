using System;
using System.Collections.Generic;

namespace RP.DAL.Repository
{
    public class SaveResult
    {
        public object Entity { get; set; }
        protected SaveResult(bool success)
        {
            _succeeded = success;
        }

        public SaveResult(IEnumerable<string> errors)
        {
            _errors = errors;
            _succeeded = false;
        }

        public SaveResult(params string[] errors)
        {
            _errors = errors;
            _succeeded = false;
        }

        private readonly IEnumerable<string> _errors;

        public IEnumerable<string> Errors
        {
            get { return _errors; }
        }

        public string GetErrorsString()
        {
            if (Errors != null)
            {
                return string.Join(",", Errors);
            }
            return "";
        }

        private readonly bool _succeeded;

        public bool Succeeded { get { return _succeeded; } }


        public static SaveResult Success()
        {
            return new SaveResult(true);
        }

        public static SaveResult Success(object entity)
        {
            return new SaveResult(true) { Entity = entity };
        }

        public static SaveResult Failed(params string[] errors)
        {
            return new SaveResult(errors);
        }
        public static SaveResult Failed(IEnumerable<string> errors)
        {
            return new SaveResult(errors);
        }
    }

    public static class SaveResultEx
    {
        public static SaveResult SaveResult(this Exception ex)
        {
            var errors = new List<string>();

            if (ex != null)
            {
                if (ex.Data.Contains("Errors"))
                {
                    var dataErrors = ex.Data["Errors"] as IEnumerable<string>;
                    if (dataErrors != null)
                    {
                        errors.AddRange(dataErrors);
                    }
                }
                else
                {
                    errors.Add(ex.Message);

                    if (ex.InnerException != null)
                    {
                        errors.Add(ex.InnerException.Message);
                    }
                }
            }
            var result = new SaveResult(errors);
            return result;
        }
    }
}
