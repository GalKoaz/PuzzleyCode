using System.Collections.Generic;

/**
 * An abstract weighted graph interface.
 * This interface does not use any memory.
 * It only has two abstract functions: Neighbors and getW.
 * T = type of node in the graph.
 * @author Erel Segal-Halevi
 * @since 2020-12
 */
public interface IWGraph<T>
{
    /**
     * Returns the neighbors of a given node.
     * @param node The node whose neighbors are to be fetched.
     * @return An IEnumerable of type T containing the neighbors of the given node.
     */
    IEnumerable<T> Neighbors(T node);

    /**
     * Retrieves the weight of a given node.
     * @param node The node whose weight is to be fetched.
     * @return An integer representing the weight of the given node.
     */
    int getW(T node);
}