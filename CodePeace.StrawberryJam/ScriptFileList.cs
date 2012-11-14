using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CodePeace.StrawberryJam
{
    public class ScriptFileList : IScriptFileList
    {
        public ScriptFileList()
        {
            _scripts = new ConcurrentDictionary<string, IScriptInfo>();
        }

        private ConcurrentDictionary<string, IScriptInfo> _scripts;

        public IEnumerable<IScriptInfo> Scripts
        {
            get
            {
                return _scripts.Values;
            }
        }

        public void Add(IScriptInfo info)
        {
            _scripts.AddOrUpdate(info.UniqueKey, key =>
            {
                info.ItemOrder = _scripts.Count;

                if (info.IsInline)
                {
                    info.LastModified = DateTime.Today;
                }

                return info;
            },
            (key, existingVal) =>
            {
                if (info.IsInline)
                {
                    info.ItemOrder = existingVal.ItemOrder;
                    info.LastModified = (info.LocalPath == existingVal.LocalPath) ? existingVal.LastModified : DateTime.Now;
                    return info;
                }
                return existingVal;
            });
        }

        public IEnumerable<IScriptInfo> ScriptsForArea(string area = null)
        {
            if (!_scripts.Any())
            {
                return _scripts.Values;
            }

            if (area != null)
            {
                return _scripts.OrderBy(k => k.Value.ItemOrder).Where(s => s.Value.Area == area).Select(s => s.Value);
            }

            return _scripts.OrderBy(k => k.Value.ItemOrder).Select(s => s.Value);
        }
    }
}