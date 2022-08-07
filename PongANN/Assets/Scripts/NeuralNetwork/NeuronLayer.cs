public class NeuronLayer
{
    private NeuralNetwork m_neuralNetwork;

    #region Neurons
    private int m_neuronNb;
    public int NeuronNb
    {
        get { return m_neuronNb; }
        protected set { m_neuronNb = value; }