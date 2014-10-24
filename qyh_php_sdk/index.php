<?php
/**
 *	微信公众平台企业号验证SDK
 *  @QQ  94560716
 *  @link https://github.com/string2008/wechat
 *  @version 1.0
 *  usage:
 *   	
 *
 */

include_once "qyhwechat.class.php";
include_once "qyhmessage.class.php";

$options = array(
        'token'=>'mytoken',															//填写应用接口的Token
        'encodingaeskey'=>'encodingaeskey',											//填写加密用的EncodingAESKey
		'corpid'=>'wx111111111111111111',											//填写高级调用功能的corpid
		'secret'=>'secret',	//填写高级调用功能的secret
		'agentid'=>'3',	
		);

$wechatObj = new wechatCallbackapiIMP();
$wechatObj->responseMsg($options);

class wechatCallbackapiIMP
{
	public function responseMsg($options)
    {
		$method=$_SERVER['REQUEST_METHOD'];
		if($method=='GET')
		{
			$qyhWechat = new QYHWechat($options);
			echo $qyhWechat->valid();
		}
		else 
		{
			$postStr = $GLOBALS["HTTP_RAW_POST_DATA"];
			$qyhMessage = new QYHMessage($postStr,$options);
		}

		exit;
		
    }
}

?>