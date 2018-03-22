namespace SociallyAwesomated.Interfaces
{
	public interface IPost
	{
		string ImageUrl { get; set; }

		string Post { get; set; }

		IUser User { get; set; }
	}
}