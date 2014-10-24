<?php
/**
 *	微信公众平台企业号验证SDK
 *  @QQ  94560716
 *  @link https://github.com/binsee/wechat-php-sdk
 *  @version 1.0
 *  usage:
 *       1.仅完成文本消息的解析和回复
 *
 */
class QYHMessage
{
	private $receviedObj;
	private $qyhWechat;
	
	///
	public function __construct($receivedMsg,$options)
	{
		logTrace("QYHMessage::construct----begin");
		
		$this->qyhWechat = new QYHWechat($options);
		
		if($this->qyhWechat->decryptMsg($receivedMsg))
		{
			logTrace("QYHMessage::construct----receivedMsg:".$this->qyhWechat->receivedXML);			
			
			$this->receviedObj = simplexml_load_string($this->qyhWechat->receivedXML, 'SimpleXMLElement', LIBXML_NOCDATA);	   	    
			$msgType = $this->receviedObj->MsgType;
			
			if($msgType=='text')
			{
				$this->textMessage();
			}
			else
			{
				logTrace("QYHMessage::construct----developing!");
			}

		}
		else
		{
			logTrace("QYHMessage::construct----null");
		}	
		
		logTrace("QYHMessage::construct----end");
	}
	
	//加密回复的消息
	private function responseMessage($responseXML)
	{
		logTrace("QYHMessage::responseMessage----begin");
		if ($this->qyhWechat->encryptMsg($responseXML)) {
			// 加密成功，企业需要将加密之后的sEncryptMsg返回
			logTrace("QYHMessage::responseMessage----qyhWechat->responseMsg:".$this->qyhWechat->responseMsg);
			echo  $this->qyhWechat->responseMsg;
			exit;
		} 
		else 
		{
			logTrace("QYHMessage::responseMessage----responseMessage error.");
			exit;
		}

	}
	
	//处理文本消息
	private function textMessage()
	{
		logTrace("QYHMessage::textMessage----begin");
		$toUserName = $this->receviedObj->FromUserName;	   	
		$fromUserName = $this->receviedObj->FromUserName;	   	    
		$agentID = $this->receviedObj->AgentID;	   	    
		$content = $this->receviedObj->Content;
		$msgId = $this->receviedObj->MsgId;
		$responseXML=null;

		logTrace("QYHMessage::textMessage----Content:$content ");
		$responseXML = makeText($fromUserName,$toUserName,"echo:".$content);
		logTrace("QYHMessage::textMessage----sResponseData:$responseXML");
		$this->responseMessage($responseXML);		
		logTrace("QYHMessage::textMessage----end");
		
	}
	
	
}
?>