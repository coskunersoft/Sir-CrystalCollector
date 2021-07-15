/*
 * Author Mustafa COSKUNER
 * 
 * Coskunersoft@outlook.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Coskunerov.EventBehaviour.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GE : Attribute
    {
        public int ID;
        public GE(int _id)
        {
            ID = _id;
        }
    }
}