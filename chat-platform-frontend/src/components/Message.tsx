import IMessage from "../models/IMessage";

interface MessageProps {
    Message: IMessage;
}

function Message(props: MessageProps) {
    return (
        <li>
            <div>
                <span>10:10 AM, Today</span> &nbsp; &nbsp;
                <span>{props.Message.username}</span>
                <i></i>
            </div>
            <div>{props.Message.messageContent}</div>
        </li>
    );
}

export default Message;
