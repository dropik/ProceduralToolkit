using System.Collections;

namespace ProceduralToolkit.EditorTests.Utils
{
    public class UIUtils
    {
        public const int DEFAULT_SKIP_FRAMES = 10;

        public static IEnumerator SkipFrames()
        {
            yield return SkipFrames(DEFAULT_SKIP_FRAMES);
        }

        public static IEnumerator SkipFrames(int frames)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }
        }
    }
}
