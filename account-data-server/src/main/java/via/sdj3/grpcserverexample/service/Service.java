package via.sdj3.grpcserverexample.service;

import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.protobuf.UsersServiceGrpc;

@GrpcService
public class Service extends UsersServiceGrpc.UsersServiceImplBase {
}
