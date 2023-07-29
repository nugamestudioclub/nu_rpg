using NuRpg.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NuRpg.Services {
	public enum MessageType {
		Create,
		Read,
		Update,
		Delete,
	}

	public class Message {
		private readonly IBlackboard _content;
		private readonly IBlackboard _response;

		public MessageType Type { get; set; }

		public int Status { get; set; }

		public IBlackboard Content => _content;

		public Message(MessageType type) {
			Type = type;
			_content = new Blackboard();
			_response = new Blackboard();
		}

		public void Clear() {
			_content.Clear();
			_response.Clear();
			Status = 0;
		}
	}

	public class Messenger {
		private readonly Dictionary<object, Action<object, Message>> _handlers;
		
		public Messenger() {
			_handlers = new();
		}

		public IDictionary<object, Action<object, Message>> Handlers => _handlers;

		[Discardable]
		public bool Handle(object sender, Message message) {
			if( _handlers.TryGetValue(sender, out var handler) ) {
				handler.Invoke(sender, message);
				return true;
			}
			else {
				return false;
			}
		}

		public bool Create(object sender, Message message) {
			return false;
		}

		public bool Read(object sender, Message message) {
			return false;
		}

		public bool Update(object sender, Message message) {
			return false;
		}

		public bool Delete(object sender, Message message) {
			return false;
		}
	}
}