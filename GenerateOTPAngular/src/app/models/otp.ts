export default interface Otp {
  userId: string;
  code: number;
  timestamp: Date;
  validity: number;
}
