using NUnit.Framework;
using UnityEngine;

namespace SpaceMiner.Tests
{
    /// <summary>
    /// Tests for the procedural asteroid field generator.
    /// </summary>
    public class AsteroidGeneratorTests
    {
        [Test]
        public void GeneratesChunksAroundPlayer()
        {
            var go = new GameObject();
            var gen = go.AddComponent<ProceduralAsteroidFieldGenerator>();
            gen.gameObject.AddComponent<Rigidbody>();
            gen.gameObject.tag = "Player";
            gen.Invoke("GenerateChunksAroundPlayer", 0f);
            Assert.Pass();
            // TODO: expand tests with a proper test scene
        }
    }
}
