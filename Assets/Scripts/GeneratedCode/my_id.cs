using UnityEngine;
using System;
using System.Collections.Generic;
using ThinkEngine.Mappers;
using static ThinkEngine.Mappers.OperationContainer;
using Unity.Multiplayer.Samples.BossRoom;
namespace ThinkEngine
{
	public class my_id : Sensor
	{
		private int counter;
		private object specificValue;
		private Operation operation;
		private BasicTypeMapper mapper;
		private List<ulong> values = new List<ulong>();
		public override void Initialize(SensorConfiguration sensorConfiguration)
		{
			this.gameObject = sensorConfiguration.gameObject;
			ready = true;
			int index = gameObject.GetInstanceID();
			mapper = (BasicTypeMapper)MapperManager.GetMapper(typeof(ulong));
			operation = mapper.OperationList()[0];
			counter = 0;
			mappingTemplate = "my_id(observer,objectIndex("+index+"),{0})." + Environment.NewLine;
		}
		public override void Destroy()
		{
		}
		public override void Update()
		{
			if(!ready)
			{
				return;
			}
			if(!invariant || first)
			{
				first = false;
				Info Info_1 = gameObject.GetComponent<Info>();
				if(Info_1 == null)
				{
					values.Clear();
					return;
				}
				if(Info_1 == null)
				{
					values.Clear();
					return;
				}
				ulong m_id_2 = Info_1.m_id;
				if (values.Count == 1)
				{
					values.RemoveAt(0);
				}
				values.Add(m_id_2);
			}
		}
		public override string Map()
		{
			object operationResult = operation(values, specificValue, counter);
			if(operationResult != null)
			{
				return string.Format(mappingTemplate, BasicTypeMapper.GetMapper(operationResult.GetType()).BasicMap(operationResult));
			}
			else
			{
				return "";
			}
		}
	}
}