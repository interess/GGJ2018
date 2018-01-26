Partial static class Env can be used to store some primitive settings, and have access to them via Editor Window.

You can find Window at "Tools / Env".

You can add public static string, bool, int and float fields to another declarations of partial class Env, and they will be displayed in the Editor Window.
You can also create System.Action fields and assign methods to them, they will be shown as buttons.

Private static string will result in bold header in Env Window.

Values are stored in EditorPrefs, so they will not be synced across different instances of Editor or different developers.
