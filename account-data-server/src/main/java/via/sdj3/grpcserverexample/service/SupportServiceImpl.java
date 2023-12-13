package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.MessageEntity;
import via.sdj3.grpcserverexample.repository.MessageRepository;
import via.sdj3.protobuf.messages.*;

import java.util.List;
import java.util.Optional;

@GrpcService
public class SupportServiceImpl extends SupportServiceGrpc.SupportServiceImplBase {
    private MessageRepository messageRepository;

    public SupportServiceImpl(MessageRepository messageRepository) {
        this.messageRepository = messageRepository;
    }

    @Override
    public void requestSupport(SupportRequest request, StreamObserver<SupportResponse> responseObserver) {
        try {
            MessageEntity messageEntity = new MessageEntity();
            messageEntity.setRequest(request.getMessage());
            messageEntity.setCustomer_id(request.getCustomerId());
            MessageEntity message =  messageRepository.save(messageEntity);
            GrpcMessage grpcMessage = GrpcMessage.newBuilder()
                    .setMessage(message.getRequest())
                    .setCustomerId(message.getCustomer_id())
                    .setIsAnswered(false)
                    .setId(message.getId())
                    .setResponse("")
                    .build();
            responseObserver.onNext(SupportResponse.newBuilder().setMessage(grpcMessage).build());
            responseObserver.onCompleted();
        }
        catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error Requesting Support"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }

    @Override
    public void provideSupport(ProvideSupportRequest request, StreamObserver<ProvideSupportResponse> responseObserver) {
        try {
            MessageEntity messageEntity = null;
            Optional<MessageEntity> optionalMessage = messageRepository.getMessageById(request.getMessageId());
            if(optionalMessage.isPresent())
            {
                messageEntity = optionalMessage.get();
            }
            if(messageEntity == null)
            {
                Status status = Status.INTERNAL.withDescription("Error Providing Support"); //message in case on error
                responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
            }
            assert messageEntity != null;
            messageEntity.setAnswered(true);
            messageEntity.setResponse(request.getResponse());
            messageRepository.save(messageEntity);
            responseObserver.onNext(ProvideSupportResponse.newBuilder().setResult("Support Provided").build());
            responseObserver.onCompleted();
        }
        catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error Providing Support"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }

    @Override
    public void getAll(GetAllRequest request, StreamObserver<GetAllResponse> responseObserver) {
        try {
            List<MessageEntity> messagesFromRepository = messageRepository.findAll();
            GetAllResponse.Builder response = GetAllResponse.newBuilder();
            for(MessageEntity messageEntity : messagesFromRepository) {
                if(messageEntity.getResponse() == null || messageEntity.getResponse().isEmpty()) {
                    GrpcMessage message = GrpcMessage.newBuilder()
                            .setId(messageEntity.getId())
                            .setMessage(messageEntity.getRequest())
                            .setCustomerId(messageEntity.getCustomer_id())
                            .setIsAnswered(messageEntity.isAnswered()).build();
                    response.addMessages(message);
                }
                else {
                    GrpcMessage message = GrpcMessage.newBuilder()
                            .setId(messageEntity.getId())
                            .setMessage(messageEntity.getRequest())
                            .setCustomerId(messageEntity.getCustomer_id())
                            .setResponse(messageEntity.getResponse())
                            .setIsAnswered(messageEntity.isAnswered()).build();
                    response.addMessages(message);
                }
            }
            responseObserver.onNext(response.build());
            responseObserver.onCompleted();
        }
        catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error Getting All"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }
}
