namespace nTestSystem.Framework.Commons
{
	/// <summary>
	/// Custom data return type
	/// (自定义数据返回类型)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Result<T>
	{

        public Result(bool status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        //The message returned by the execution result
        //执行结果所返回的消息
        public string Message { private set; get; }

        //The status returned by the execution result
        //执行结果所返回的状态
        public bool Status { private set; get; }

        //The data returned by the execution result
        //执行结果所返回的数据        
        public T Data { private set; get; }

        //Return success
        //返回成功
        public static Result<T> Success(string message="", T data = default)
        {
            return new Result<T>(true, message, data);
        }

        //Return failed
        //返回失败
        public static Result<T> Error(string message, T data = default)
        {
            return new Result<T>(false, message, data);
        }
    }
}
