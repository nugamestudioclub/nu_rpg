namespace NuRpg.Navigation {
	public interface ILocator<P, D> {
		bool Exists(P point);
		bool Exists(P origin, D direction);
		P Locate(P origin, D direction);
		bool TryLocate(P origin, D direction, out P result);
	}
}