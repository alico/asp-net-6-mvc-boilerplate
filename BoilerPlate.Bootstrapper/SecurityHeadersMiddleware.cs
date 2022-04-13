using Microsoft.AspNetCore.Http;

namespace BoilerPlate.Bootstrapper
{
    public sealed class SecurityHeadersMiddleware
    {

        private const string PermissionPolicy = "accelerometer=();" +
                                                "ambient-light-sensor=();" +
                                                "autoplay=('self');" +
                                                "battery=();" +
                                                "camera=('self');" +
                                                "display-capture=();" +
                                                "document-domain=();" +
                                                "encrypted-media=();" +
                                                "execution-while-not-rendered=();" +
                                                "execution-while-out-of-viewport=();" +
                                                "gyroscope=();" +
                                                "magnetometer=();" +
                                                "microphone=('self');" +
                                                "midi=();" +
                                                "navigation-override=();" +
                                                "payment=();" +
                                                "picture-in-picture=();" +
                                                "publickey-credentials-get=();" +
                                                "sync-xhr=();" +
                                                "usb=();" +
                                                "wake-lock=();" +
                                                "xr-spatial-tracking=();";

        private const string CSP = "base-uri 'none';" +
                                   "block-all-mixed-content;" +
                                   "child-src 'none';" +
                                   "connect-src 'self';" +
                                   "default-src 'self';" +
                                   "font-src 'self';" +
                                   "form-action 'self';" +
                                   "frame-ancestors 'none';" +
                                   "frame-src " +
                                   "img-src 'self' data:;" +
                                   "manifest-src 'none';" +
                                   "media-src 'self';" +
                                   "object-src 'self';" +
                                   "sandbox;" +
                                   "script-src 'self' 'unsafe-inline' ;" +
                                   "script-src-attr 'self';" +
                                   "script-src-elem 'self' 'unsafe-inline' ;" +
                                   "style-src 'self';" +
                                   "style-src-attr 'self' 'unsafe-inline';" +
                                   "style-src-elem 'self';" +
                                   "upgrade-insecure-requests;" +
                                   "worker-src 'none';";

        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    }
}
