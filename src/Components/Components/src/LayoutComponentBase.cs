// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components
{
    /// <summary>
    /// Optional base class for components that represent a layout.
    /// Alternatively, components may implement <see cref="IComponent"/> directly
    /// and declare their own parameter named <see cref="Body"/>.
    /// </summary>
    public abstract class LayoutComponentBase : ComponentBase
    {
        internal const string BodyPropertyName = nameof(Body);

        /// <summary>
        /// Gets the content to be rendered inside the layout.
        /// </summary>
        [Parameter]
        public RenderFragment? Body { get; set; }

        /// <inheritdoc />
        public override Task SetParametersAsync(ParameterView parameters)
        {
            // Derived instances of LayoutComponentBase do not appear in any statically analyzable
            // calls of OpenComponent<T> where T is well-known. As a result, relying on reflection
            // (ComponentProperties.SetProperties) will result in the setter being trimmed away since
            // it appears unused. Statically reference it to ensure the setter is preserved.
            if (parameters.TryGetValue(BodyPropertyName, out RenderFragment? body))
            {
                Body = body;
            }

            return base.SetParametersAsync(parameters);
        }
    }
}
