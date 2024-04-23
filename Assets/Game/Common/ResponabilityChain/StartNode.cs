using UnityEngine;

namespace Game.Common.ResponabilityChain
{
    public sealed class StartNode<T> : AbstractNodeHandler<T> where T : MonoBehaviour
    {
    }
}