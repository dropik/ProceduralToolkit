using UnityEngine;
using UnityEngine.UIElements;

namespace ProceduralToolkit.EditorTests.Utils
{
    public class ButtonClicker
    {
        private Button button;

        public void Click(Button button)
        {
            this.button = button;
            if (button != null)
            {
                SendPointerEvent<PointerDownEvent>();
                SendPointerEvent<PointerUpEvent>();
            }
        }

        private void SendPointerEvent<TEvent>()
            where TEvent : PointerEventBase<TEvent>, new()
        {
            using (var e = CreatePointerEvent<TEvent>())
            {
                ExecuteSendEvent(e);
            }
        }

        private TEvent CreatePointerEvent<TEvent>()
            where TEvent : PointerEventBase<TEvent>, new()
        {
            return PointerEventBase<TEvent>.GetPooled(CreateSystemEventOfType(EventType.MouseDown));
        }

        private Event CreateSystemEventOfType(EventType eventType)
        {
            return new Event()
            {
                button = (int)MouseButton.LeftMouse,
                clickCount = 1,
                type = eventType,
                pointerType = UnityEngine.PointerType.Mouse,
                mousePosition = MakeMousePositionInsideButton()
            };
        }

        private Vector2 MakeMousePositionInsideButton()
        {
            return new Vector2(button.worldBound.x + 1, button.worldBound.y + 1);
        }

        private void ExecuteSendEvent<TEvent>(TEvent e)
            where TEvent : PointerEventBase<TEvent>, new()
        {
            e.target = button;
            button.SendEvent(e);
        }
    }
}
