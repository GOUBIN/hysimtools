using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    class HRT_TimeTickObject
    {
        public static Queue<HRT_TimeTickObject> _pool = new Queue<HRT_TimeTickObject>();
        public static HRT_TimeTickObject Get()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue();
            }
            else
            {
                var o = new HRT_TimeTickObject(string.Empty, null, float.MaxValue, -1);
                return o;
            }
        }
        public static void Return(HRT_TimeTickObject o)
        {
            o.Reset();
            _pool.Enqueue(o);
        }
        public string name;
        public System.Action action;
        public float ttr;
        public float _cttr;
        public int runTime;
        public bool needDestory;
        public HRT_TimeTickObject countTtr(float delta)
        {
            this._cttr -= delta;
            if (this._cttr <= 0)
            {
                this._cttr = this.ttr; //重设时间
                if (this.action != null)
                {
                    //try
                    //{
                        this.action();
                    //}
                    //catch (System.Exception ex)
                    //{
                       
                    //    this.needDestory = true;
                    //}
                }
                if (this.runTime > 0)
                {
                    this.runTime--;
                    if (this.runTime <= 0)
                    {
                        this.needDestory = true;
                    }
                }
            }
            return this;
        }
        public void Reset()
        {
            this.name = null;
            this.action = null;
            this.ttr = float.MaxValue;
            this._cttr = this.ttr;
            this.runTime = -1;
            this.needDestory = false;
        }
        public HRT_TimeTickObject(string name, System.Action action, float timeStep, int runTime)
        {
            this.name = name;
            this.action = action;
            this.ttr = timeStep;
            this._cttr = this.ttr;
            this.runTime = runTime;
            this.needDestory = false;
        }
        public HRT_TimeTickObject(string name, System.Action action, float timeStep, float timeDelayed, int runTime)
        {
            this.name = name;
            this.action = action;
            this.ttr = timeStep;
            this._cttr = timeDelayed;
            this.runTime = runTime;
            this.needDestory = false;
        }
    }
    public class TimeTick : Singleton<TimeTick>
    {
        int name_idx = 0;
        public int getName()
        {
            return name_idx == int.MaxValue ? 0 : ++name_idx;
        }
        Dictionary<string, HRT_TimeTickObject> _action_pool = new Dictionary<string, HRT_TimeTickObject>();
        List<string> _remove_action = new List<string>();
        List<string> _running_action = new List<string>();
        /// <summary>
        /// 采取系统提供的计时器
        /// </summary>
        /// <param name="执行回调"></param>
        /// <param name="执行间隔"></param>
        /// <param name="执行次数(默认为单次执行"></param>
        /// <returns></returns>
        public int SetAction(System.Action action, float ttr, int time = 1)
        {
            int randomIndex = getName();
            SetAction(randomIndex.ToString(), action, ttr, time);
            return randomIndex;
        }
        /// <summary>
        /// 设置计时器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="ttr"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string SetAction(string name, System.Action action, float ttr, int time = 1)
        {
            if (_action_pool.ContainsKey(name))
            {
                RemoveAction(name, true);
            }
            var o = HRT_TimeTickObject.Get();
            o.name = name.ToString();
            o.action = action;
            o.ttr = ttr;
            o._cttr = o.ttr;
            o.runTime = time;
            _action_pool.Add(o.name, o);
            _running_action.Add(o.name);
            return name;
        }
        /// <summary>
        /// 设置计时器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <param name="ttr"></param>
        /// <param name="cttr"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string SetAction(string name, System.Action action, float ttr, float cttr, int time = 1)
        {
            if (_action_pool.ContainsKey(name))
            {
                RemoveAction(name, true);
            }
            var o = HRT_TimeTickObject.Get();
            o.name = name.ToString();
            o.action = action;
            o.ttr = ttr;
            o._cttr = cttr;
            o.runTime = time;
            _action_pool.Add(o.name, o);
            _running_action.Add(o.name);
            return name;
        }
        /// <summary>
        /// 删除定时器
        /// </summary>
        /// <param name="action"></param>
        public void RemoveAction(System.Action action)
        {
            if (action == null)
            {
                return;
            }
            for (int i = 0; i < this._running_action.Count; i++)
            {
                var name = this._running_action[i];
                if (_action_pool[name].action.Equals(action))
                {
                    _remove_action.Add(name);
                }
            }
            if (_remove_action.Count > 0)
            {
                for (int i = 0; i < _remove_action.Count; i++)
                {
                    RemoveAction(_remove_action[i], true);
                }
                _remove_action.Clear();
            }
        }
        public delegate void TimeTickTimeOutDelegate(string index);
        public TimeTickTimeOutDelegate TimeOutCall;
        /// <summary>
        /// 删除定时器底层
        /// </summary>
        /// <param name="name"></param>
        /// <param name="是否为主动请求停止(当为True时  不发送结束回调"></param>
        public void RemoveAction(string name, bool outSend)
        {
            if (_action_pool.ContainsKey(name))
            {
                var o = _action_pool[name];
                HRT_TimeTickObject.Return(o);
                _action_pool.Remove(name);
                _running_action.Remove(name);
                if (!outSend && TimeOutCall != null)
                {
                    //非外部调用，调用回调
                    TimeOutCall(name);
                }
            }
        }
        public void RemoveAction(int name, bool outSend)
        {
            RemoveAction(name.ToString(), outSend);
        }
        public void TickTick(float delta)
        {
            //if(_action_pool.)          
            for (int i = 0; i < _running_action.Count; i++)
            {
                string name = _running_action[i];
                if (_action_pool[_running_action[i]].countTtr(delta).needDestory)
                {
                    _remove_action.Add(name);
                }
            }
            if (_remove_action.Count > 0)
            {
                for (int i = 0; i < _remove_action.Count; i++)
                {
                    RemoveAction(_remove_action[i], false);
                }
                _remove_action.Clear();
            }
        }
    }
