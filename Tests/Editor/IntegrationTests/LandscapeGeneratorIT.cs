using NUnit.Framework;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IntegrationTests
{
    public class LandscapeGeneratorIT
    {
        private LandscapeGenerator landscapeGenerator;
        private Mesh resultingMesh;

        [SetUp]
        public void SetUp()
        {
            landscapeGenerator = new GameObject().AddComponent<LandscapeGenerator>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(landscapeGenerator.gameObject);
        }

        [Test]
        public void TestGenerateWithPlane()
        {
            SetupGeneratorWithPlane();

            landscapeGenerator.Generate();

            AssertThatMeshWasGeneratedCorrectly();
            AssertThatLandscapeHasSomeMaterial();
        }

        private void SetupGeneratorWithPlane()
        {
            landscapeGenerator.transform.position = Vector3.zero;
            var plane = new PlaneGenerator(Vector3.zero, 2, 1);
            landscapeGenerator.Generator = plane;
        }

        private void AssertThatMeshWasGeneratedCorrectly()
        {
            GetResultingMesh();
            AssertThatVerticesAreCorrect();
            AssertThatTrianglesAreCorrect();
        }

        private void GetResultingMesh()
        {
            resultingMesh =  landscapeGenerator.GetComponent<MeshFilter>().sharedMesh;
        }

        private void AssertThatVerticesAreCorrect()
        {
            var expectedVertices = new Vector3[]
            {
                new Vector3(-1, 0, 0.5f),
                new Vector3(1, 0, 0.5f),
                new Vector3(-1, 0, -0.5f),
                new Vector3(-1, 0, -0.5f),
                new Vector3(1, 0, 0.5f),
                new Vector3(1, 0, -0.5f)
            };
            CollectionAssert.AreEqual(expectedVertices, resultingMesh.vertices);
        }

        private void AssertThatTrianglesAreCorrect()
        {
            var expectedTriangles = new int[] { 0, 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(expectedTriangles, resultingMesh.triangles);
        }

        private void AssertThatLandscapeHasSomeMaterial()
        {
            var resultingMaterial = landscapeGenerator.GetComponent<MeshRenderer>().sharedMaterial;
            Assert.That(resultingMaterial != null);
        }
    }
}
