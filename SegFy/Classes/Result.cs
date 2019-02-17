using System;

namespace SegFy
{
    [Serializable]
    public class Result
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public int Code { get; set; }
        public string URL { get; set; }

        public Result() {
            
        }

        public void setSuccess()
        {
            setSuccess(null);
        }
        public void setSuccess(string message)
        {
            Error = false;
            Message = string.IsNullOrEmpty(message) ? "Ação realizada com sucesso!" : message;
        }
        public void setSuccess(string message, string url)
        {
            Error = false;
            Message = string.IsNullOrEmpty(message) ? "Ação realizada com sucesso!" : message;
            URL = url;
        }

        public void setError()
        {
            setError(null, 0, null);
        }
        public void setError(string message)
        {
            setError(message, 0, null);
        }
        public void setError(string message, string url)
        {
            setError(message, 0, url);
        }
        public void setError(int code)
        {
            setError(null, code, null);
        }
        public void setError(int code, string url)
        {
            setError(null, code, url);
        }
        public void setError(string message, int code, string url)
        {
            Error = true;
            Code = code;
            Message = string.IsNullOrEmpty(message) ? "Ocorreu um erro inesperado!" : message;
            URL = url;
        }
    }

    [Serializable]
    public class Result<T> : Result where T : class
    {
        public T Object { get; set; }
    }
}