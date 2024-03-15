using UnityEngine;
using System.Collections.Generic;

namespace KingOfGuns.Core
{
    public class Level : MonoBehaviour
    {
        private List<IReloadable> _reloadables = new List<IReloadable>();

        public void Register(IReloadable reloadable)
        {
            if (!_reloadables.Contains(reloadable))
                _reloadables.Add(reloadable);
        }

        public void Unregister(IReloadable reloadable)
        {
            if (_reloadables.Contains(reloadable))
                _reloadables.Remove(reloadable);
        }

        public void Reload() => _reloadables.ForEach(reloadable => reloadable.Reload());
    }
}