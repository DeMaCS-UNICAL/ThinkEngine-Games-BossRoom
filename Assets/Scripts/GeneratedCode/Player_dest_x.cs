using UnityEngine;
using System;
using System.Collections.Generic;
using ThinkEngine.Mappers;
using static ThinkEngine.Mappers.OperationContainer;
using Unity.Multiplayer.Samples.BossRoom;
namespace ThinkEngine
{
	public class Player_dest_x : Sensor
	{
		private int counter;
		private object specificValue;
		private Operation operation;
		private BasicTypeMapper mapper;
		private List<int> values = new List<int>();
		public override void Initialize(SensorConfiguration sensorConfiguration)
		{
			this.gameObject = sensorConfiguration.gameObject;
			ready = true;
			int index = gameObject.GetInstanceID();
			mapper = (BasicTypeMapper)MapperManager.GetMapper(typeof(int));
			operation = mapper.OperationList()[0];
			counter = 0;
			mappingTemplate = "player_dest_x(playerAvatar,objectIndex("+index+"),{0})." + Environment.NewLine;
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
				FloattoInt FloattoInt_1 = gameObject.GetComponent<FloattoInt>();
				if(FloattoInt_1 == null)
				{
					values.Clear();
					return;
				}
				if(FloattoInt_1 == null)
				{
					values.Clear();
					return;
				}
				int dest_x_2 = FloattoInt_1.dest_x;
				if (values.Count == 1)
				{
					values.RemoveAt(0);
				}
				values.Add(dest_x_2);
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