using UnityEngine;

namespace Game.Common.Filters
{
    public interface IFilter
    {
        bool Check(GameObject obj);
    }
}