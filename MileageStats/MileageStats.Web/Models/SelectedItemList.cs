//===================================================================================
// Microsoft patterns & practices
// Silk : Web Client Guidance
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.Collections.Generic;
using System.Linq;

namespace MileageStats.Web.Models
{
    public class SelectedItemList<T> : IEnumerable<T>
    {
        public T this[int i]
        {
            get { return List.ToArray()[i]; }
        }

        public SelectedItemList(IEnumerable<T> source, Func<IEnumerable<T>, T> setSelected)
            : this(source, setSelected(source))
        {
        }

        public SelectedItemList(IEnumerable<T> source) : this(source, default(T))
        {
        }

        public SelectedItemList(IEnumerable<T> source, T selectedItem)
        {
            List = source;
            SelectedItem = selectedItem;
        }

        public IEnumerable<T> List { get; private set; }
        public T SelectedItem { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}