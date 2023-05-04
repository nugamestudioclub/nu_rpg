using System;
using System.Collections.Generic;

namespace NuRpg.Navigation {
	public interface IPathFinder<P> {
		IEnumerable<P> FindPath(P from, P to);
		IEnumerable<P> FindPath(P from, P to, Func<P, P, bool> isAllowed);
		IEnumerable<P> FindPath(IList<P> points, Func<P, P, bool> isAllowed);
		IEnumerable<P> FindPath(IList<P> points);
	}
}