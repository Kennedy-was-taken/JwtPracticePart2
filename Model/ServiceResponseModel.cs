namespace JwtPracticePart2.Model{

    public class ServiceResponseModel <T>
    {
        public T? Data{get; set;}

        public Boolean isSuccessful {get; set;}

        public string? Message {get; set;}
    }
}