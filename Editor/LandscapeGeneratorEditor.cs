using UnityEditor;
using UnityEngine.UIElements;

namespace ProceduralToolkit
{
    [CustomEditor(typeof(LandscapeGenerator))]
    [LayoutPath("Layouts/LandscapeGeneratorEditor")]
    public class LandscapeGeneratorEditor : EditorBase<LandscapeGeneratorEditor>
    {
        private LandscapeGenerator generator;
        private const float SLIDER_STEP = 0.01f;

        public int PlaneLengthSlider
        {
            get
            {
                return (int)(generator.planeLength / SLIDER_STEP);
            }
            set
            {
                generator.planeLength = value * SLIDER_STEP;
            }
        }

        private void Awake()
        {
            generator = target as LandscapeGenerator;
        }

        protected override void AddCallbacksToVisualElements()
        {
            var slider = new Slider();
        }
    }
}
