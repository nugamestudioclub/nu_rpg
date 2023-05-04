namespace NuRpg.Navigation {
	public interface ILocator<P, D> {
		P Locate(P origin, D direction);
	}
}