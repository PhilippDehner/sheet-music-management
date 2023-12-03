namespace Backend.Types;

public class Person
{
	public uint Id { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public uint Version { get; set; }

	public Person()
	{
		LastName = string.Empty;
		FirstName = string.Empty;
	}
}
