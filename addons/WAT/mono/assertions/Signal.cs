using System.Collections;
using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;
using Object = Godot.Object;

namespace WAT {
    public class Signal : Assertion {
        public static Dictionary WasEmitted(Object emitter, string signal, string context) {
            string passed = $"Signal {signal} was emitted from {emitter}";
            string failed = $"Signal {signal} was not emitted from {emitter}";

            var watcher = (Reference)emitter.Call("get_meta", "watcher");
            bool success = (int)watcher.Call("get_emit_count", signal) > 0;
            string result = success ? passed : failed;

            return Result(success, passed, result, context);
        }

        public static Dictionary WasNotEmitted(Object emitter, string signal, string context) {
            string passed = $"Signal {signal} was not emitted from {emitter}";
            string failed = $"Signal {signal} was emitted from {emitter}";

            var watcher = (Reference)emitter.GetMeta("watcher");
            bool success = (int)watcher.Call("get_emit_count", signal) <= 0;
            string result = success ? passed : failed;

            return Result(success, passed, result, context);
        }

        public static Dictionary WasEmittedXTimes(Object emitter, string signal, int times, string context) {
            string passed = $"Signal {signal} was emitted {times} times from {emitter}";
            string failed = $"Signal {signal} was not emitted {times} times from {emitter}";

            var watcher = (Reference)emitter.GetMeta("watcher");
            bool success = (int)watcher.Call("get_emit_count", signal) == times;
            string result = success ? passed : failed;

            return Result(success, passed, result, context);
        }

        public static Dictionary WasEmittedWithArgs(Object emitter, string signal, Array arguments, string context) {
            string passed = $"Signal {signal} was emitted from {emitter} with arguments {arguments}";
            string failed = $"Signal {signal} was not emitted from {emitter} with arguments {arguments}";
            string altFailure = $"Signal {signal} was not emitted from {emitter}";

            bool success = false;
            string result = "";
            var watcher = (Reference)emitter.GetMeta("watcher");
            var data = (IDictionary)watcher.Call("get_data", signal);
            if ((int)data["emit_count"] <= 0) {
                success = false;
                result = altFailure;
            } else if (FoundMatchingCall(arguments, (IEnumerable)data["calls"])) {
                success = true;
                result = passed;
            } else {
                success = false;
                result = failed;
            }

            return Result(success, passed, result, context);
        }

        static bool FoundMatchingCall(IList args, IEnumerable calls) {
            foreach (IDictionary call in calls) {
                if (Match(args, (Array)call["args"])) {
                    return true;
                }
            }

            return false;
        }

        static bool Match(IList arguments, IList callArguments) {
            for (int i = 0; i < arguments.Count; i++) {
                if (!Equals(arguments[i], callArguments[i])) {
                    return false;
                }
            }
            return true;
        }
    }
}
