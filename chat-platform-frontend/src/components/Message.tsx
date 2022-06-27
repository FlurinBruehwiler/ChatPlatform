import IMessage from "../models/IMessage";

interface MessageProps {
    Message: IMessage;
}

function Message(props: MessageProps) {
    return (
        <li className={"bg-amber-200 mb-2 rounded-md w-[300px] p-3"}>
            <div>
                <span>{props.Message.messageId}</span> &nbsp; &nbsp;
                <span>{props.Message.username}</span>
                <i></i>
            </div>
            <div>{props.Message.messageContent}</div>
        </li>
    );
}

export default Message;
