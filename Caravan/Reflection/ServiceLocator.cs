//
// ServiceLocator.cs
// 
// Author(s):
//     Alessio Parma <alessio.parma@gmail.com>
//
// The MIT License (MIT)
// 
// Copyright (c) 2014-2016 Alessio Parma <alessio.parma@gmail.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Concurrent;
using Finsa.Caravan.Diagnostics;

namespace Finsa.Caravan.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ServiceLocator
    {
        private static readonly ConcurrentDictionary<Type, object> InstanceCache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullyQualifiedTypeName"></param>
        /// <returns></returns>
        public static T Load<T>(string fullyQualifiedTypeName) where T : class
        {
            // If cache contains an instance of the type, we return it.
            var type = typeof(T);
            object instance;
            if (InstanceCache.TryGetValue(type, out instance)) {
                return instance as T;
            }

            // We load the class and we make sure it implements T.
            Type klass;
            try {
                klass = Type.GetType(fullyQualifiedTypeName, true, true);
            } catch (Exception ex) {
                throw new ArgumentException(ErrorMessages.Reflection_ServiceLocator_ErrorOnLoading, "fullyQualifiedTypeName", ex);
            }
            Raise<ArgumentException>.IfIsNotContainedIn(type, klass.GetInterfaces(), ErrorMessages.Reflection_ServiceLocator_InterfaceNotImplemented);
            instance = Activator.CreateInstance(klass);

            // We store the executor instance inside the cache, and then we return it.
            InstanceCache.TryAdd(type, instance);
            return instance as T;
        }
    }
}