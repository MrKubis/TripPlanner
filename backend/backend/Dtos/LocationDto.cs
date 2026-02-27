namespace backend.Dtos;

public class LocationDto
{
    public double Latitude {get; set;}
    public double Longitude {get; set;}
}

public class CreateLocationDto
{
    public double Latitude {get; set;}
    public double Longitude {get; set;}
}

public class PatchLocationDto
{
    public double Latitude {get; set;}
    public double Longitude {get; set;}
}