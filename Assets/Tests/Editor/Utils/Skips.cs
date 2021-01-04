using System.Collections;

namespace ProceduralToolkit.EditorTests.Utils
{
    public static class Skips
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

        public static IEnumerator SkipSeconds(float seconds)
        {
            var start = GetMilliseconds();
            var end = GetMilliseconds();
            while (end - start < seconds * 1000)
            {
                yield return null;
                end = GetMilliseconds();
            }
        }

        private static long GetMilliseconds()
        {
            return System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        }
    }
}
