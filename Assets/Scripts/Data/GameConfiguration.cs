using System;
using UnityEngine;

namespace RedRift.Test.Data
{
    [CreateAssetMenu (menuName = "GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField] private Configuration _configuration;
        public Configuration Config => _configuration;
    }
}

[Serializable]
public struct Configuration
{
    public string UrlImageResources;
    [Space]
    public int MinCountCard;
    public int MaxCountCard;
    [Space]
    public int MinRandomChange;
    public int MaxRandomChange;
    [Space]
    public int MinHpForDestroyCard;
}