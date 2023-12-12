package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.MessageEntity;
import via.sdj3.grpcserverexample.repository.MessageRepository;
import via.sdj3.protobuf.messages.*;

@GrpcService
public class SupportService extends SupportServiceGrpc.SupportServiceImplBase {
    private MessageRepository messageRepository;

    public SupportService(MessageRepository messageRepository) {
        this.messageRepository = messageRepository;
    }

    @Override
    public void requestSupport(SupportRequest request, StreamObserver<SupportResponse> responseObserver) {
        try {
            MessageEntity messageEntity = new MessageEntity();
            messageEntity.setRequest(request.getMessage());
            messageEntity.setCustomer_id(request.getCustomerId());
            messageRepository.save(messageEntity);
            responseObserver.onNext(SupportResponse.newBuilder().setResult("Support Requested!").build());
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
            MessageEntity messageEntity = messageRepository.getReferenceById(request.getMessageId());
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
}
