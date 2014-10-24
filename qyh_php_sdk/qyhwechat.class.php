<?php
/**
 *	微信公众平台企业号验证SDK, 官方API类库
 *  @QQ  94560716
 *  @link https://github.com/binsee/wechat-php-sdk
 *  @version 1.0
 *  usage:
 *   	1.仅完成文本消息的解析和回复
 *      2.如果验证有问题，请检查log.txt
 *
 */
include_once("qyhmessage.php");
include_once("qyhlib/WXBizMsgCrypt.php");

class QYHWechat
{
	private $token;
	private $encodingAesKey;
	private $corpid;         //也就是企业号的CorpID
	private $secret;
    private $agentid;       //应用id

	public  $responseMsg ="";
	public  $receivedXML = "";
	public  $toUsername = "";
	
	public 	$errCode = 40001;
	public 	$errMsg = "no access";
	
	public  $postResponse = "";
	
	private $wxcpt;
	
	public function __construct($options)
	{
		logTrace("QYHWechat::construct----begin");
		
		$this->token = isset($options['token'])?$options['token']:'';
		$this->encodingAesKey = isset($options['encodingaeskey'])?$options['encodingaeskey']:'';
		$this->corpid =  isset($options['corpid'])?$options['corpid']:'';
		$this->secret = isset($options['secret'])?$options['secret']:'';
		$this->agentid = isset($options['agentid'])?$options['agentid']:'';
		
		$this->wxcpt = new WXBizMsgCrypt($this->token, $this->encodingAesKey, $this->corpid);
		logTrace("QYHWechat::construct----token:".$this->token." encodingAesKey:" .$this->encodingAesKey." corpid:".$this->corpid);
		
		logTrace("QYHWechat::construct----end");
	}
	
	public function valid()
    {
		logTrace("QYHWechat::valid----begin");
		$sVerifyMsgSig = isset($_GET["msg_signature"])? $_GET["msg_signature"]:'b8d323e4ae0193a633c989358fae6c198bb0827d';
		$sVerifyTimeStamp = isset($_GET["timestamp"])? $_GET["timestamp"]:'1413279955';
		$sVerifyNonce = isset($_GET["nonce"])? $_GET["nonce"]:'2070296983';	
		$sVerifyEchoStr = isset($_GET["echostr"])? $_GET["echostr"]:'This is a test!';	

		$sEchoStr="";
		
		$errCode = $this->wxcpt->VerifyURL($sVerifyMsgSig, $sVerifyTimeStamp, $sVerifyNonce, $sVerifyEchoStr, $sEchoStr);
		if ($errCode == 0) {
			//
			// 验证URL成功，将sEchoStr返回
			// HttpUtils.SetResponce($sEchoStr);
			logTrace("QYHWechat::valid----sEchoStr:$sEchoStr");
			return $sEchoStr;
		} else {
			logTrace("QYHWechat::valid----errCode:$errCode");
		}
    }

	public function decryptMsg($receivedMsg)
	{
		logTrace("QYHWechat::decryptMsg----begin");
		
		$verifyMsgSig = isset($_GET["msg_signature"])? $_GET["msg_signature"]:'b8d323e4ae0193a633c989358fae6c198bb0827d';
		$verifyTimeStamp = isset($_GET["timestamp"])? $_GET["timestamp"]:'1413279955';
		$verifyNonce = isset($_GET["nonce"])? $_GET["nonce"]:'2070296983';	
		
		logTrace("QYHWechat::decryptMsg----sReqMsgSig:$verifyMsgSig");
		logTrace("QYHWechat::decryptMsg----sReqTimeStamp:$verifyTimeStamp");
		logTrace("QYHWechat::decryptMsg----sReqNonce:$verifyNonce");
		
		$receivedXML = "";  // 解析之后的明文
		$errCode = $this->wxcpt->DecryptMsg($verifyMsgSig, $verifyTimeStamp, $verifyNonce, $receivedMsg, $receivedXML);
		
		if($errCode == 0)
		{	
			$this->receivedXML = $receivedXML;
			return true;
		}
		else
		{
			logTrace("QYHWechat::decryptMsg----DecryptMsg errCode:$errCode");
			$this->errCode = $errCode;
			return false;
		}
	}
	
	public function encryptMsg($responseXML)
	{
		logTrace("QYHWechat::encryptMsg----begin");

		$verifyTimeStamp = time();
		$verifyNonce = "13588888888" ;
	
		$encryptMsg="";// 加密后的密文
		logTrace("QYHWechat::encryptMsg----verifyTimeStamp");
		$errCode = $this->wxcpt->EncryptMsg($responseXML, $verifyTimeStamp, $verifyNonce, $encryptMsg);
		if($errCode == 0)
		{			
			$this->responseMsg = $encryptMsg;
			return true;
		}
		else
		{
			logTrace("QYHWechat::encryptMsg----encryptMsg errCode:$errCode");
			$this->errCode = $errCode;
			return false;
		}
	}
	
}
?>