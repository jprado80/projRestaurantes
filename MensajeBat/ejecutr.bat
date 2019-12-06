net stop "Message Queuing Triggers"
regasm MessageQueueProcessor.dll /unregister
gacutil /u MessageQueueProcessor

regasm MessageQueueProcessor.dll
gacutil /i MessageQueueProcessor.dll
net start "Message Queuing Triggers"