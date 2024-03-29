import IMessage from "../models/IMessage";
import {baseUrl} from "../axios";

interface MessageProps {
    Message: IMessage;
}

function Message(props: MessageProps) {
    return (
        <li className={"bg-amber-200 mb-2 rounded-md w-[300px] p-3"}>
            <div>
                <b>{props.Message.username}</b>
                <i></i>
            </div>
            <div>{props.Message.messageContent}</div>
            {props.Message.image && <img src={`${baseUrl}/${props.Message.image}`} alt="" className={"block w-auto h-auto"}/>}
        </li>
    );
}

export default Message;
