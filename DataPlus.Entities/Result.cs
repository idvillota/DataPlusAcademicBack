namespace DataPlus.Entities
{
    public class Result
    {
        public bool Sucess { get; set; }

        public string errorMessage { get; set; }

        public Result()
        {
        }

        public Result(bool success, string errorMessage)
        {
        }
    }
}
